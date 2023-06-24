using EF_Pagination_Example.Business.Interfaces;
using EF_Pagination_Example.Model;
using EF_Pagination_Example.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EF_Pagination_Example.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using EF_Pagination_Example.Data.Repositories.Interfaces;

namespace EF_Pagination_Example.Business.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly string ROLE = "User";
        private readonly string CLAIM_TYPE = "User";
        private readonly string CLAIM_VALUE_READ = "Read";
        private readonly string CLAIM_VALUE_INSERT = "Insert";
        private readonly string CLAIM_VALUE_UPDATE = "Update";
        private readonly string CLAIM_VALUE_DELETE = "Delete";
        
        private readonly ILogger<AuthService> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAspNetUser _aspNetUser;
        private readonly Token _token;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(
            INotifier notifier,
            ILogger<AuthService> logger,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IAspNetUser aspNetUser,
            IOptions<Token> token,
            IRefreshTokenRepository refreshTokenRepository) : base(notifier)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _aspNetUser = aspNetUser;
            _token = token.Value;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<LoginResponseViewModel> LoginAsync(LoginRequestViewModel loginRequestViewModel, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var appUser = await _userManager.FindByEmailAsync(loginRequestViewModel.Email).ConfigureAwait(false);
            if (appUser is null)
                return Notify("Incorrect username or password", new LoginResponseViewModel());

            var result = await _signInManager.PasswordSignInAsync(appUser, loginRequestViewModel.Password, false, true).ConfigureAwait(false);
            if (result.Succeeded)
                return await GetToken(appUser, cancellationToken).ConfigureAwait(false);

            if (result.IsLockedOut)
                return Notify("User temporarily blocked for invalid attempts.", new LoginResponseViewModel());

            return Notify("Incorrect username or password", new LoginResponseViewModel());
        }

        public async Task<LoginResponseViewModel> GenerateJwtAsync(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
            if (user is null)
                return Notify("Incorrect username or password.", new LoginResponseViewModel());

            return await GetToken(user, cancellationToken).ConfigureAwait(false);
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(Guid refreshToken, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var token = await _refreshTokenRepository.GetRefreshTokenAsync(refreshToken, cancellationToken).ConfigureAwait(false);

            return token is not null && token.ExpirationDate.ToLocalTime() > DateTime.Now
                ? token
                : null;
        }

        public async Task<AppUser> CreateUserAsync(RegisterUserViewModel registerUser, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var parts = registerUser.Email.Split("@");
                var user = new AppUser
                {
                    UserName = parts[0],
                    NormalizedUserName = parts[0].ToUpper(),
                    Email = registerUser.Email,
                    NormalizedEmail = registerUser.Email.ToUpper(),
                };

                var result = await _userManager.CreateAsync(user, registerUser.Password).ConfigureAwait(false);

                if (result.Errors.Any())
                    return Notify("Could not create user.", new AppUser());

                var claims = new List<Claim>()
                {
                    new Claim(CLAIM_TYPE, CLAIM_VALUE_READ),
                    new Claim(CLAIM_TYPE, CLAIM_VALUE_INSERT),
                    new Claim(CLAIM_TYPE, CLAIM_VALUE_UPDATE),
                    new Claim(CLAIM_TYPE, CLAIM_VALUE_DELETE),
                };

                var roleResult = await _userManager.AddToRoleAsync(user, ROLE).ConfigureAwait(false);
                var userClaimResult = await _userManager.AddClaimsAsync(user, claims).ConfigureAwait(false);
                return user;
            }
            catch (Exception exception)
            {
                return Notify(exception.Message, new AppUser());
            }
        }

        private async Task<LoginResponseViewModel> GetToken(AppUser user, CancellationToken cancellationToken)
        {
            var claims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);
            var identityClaims = await GetClaimsUserAsync(claims, user).ConfigureAwait(false);
            var encodedToken = EncodeToken(identityClaims);
            var refreshToken = await GenerateRefreshTokenAsync(user.Email ?? "", cancellationToken).ConfigureAwait(false);

            return GetResponseToken(encodedToken, user, claims, refreshToken);
        }

        private async Task<ClaimsIdentity> GetClaimsUserAsync(ICollection<Claim> claims, AppUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
                claims.Add(new Claim("role", userRole));

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string EncodeToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var currentIssuer = $"{_aspNetUser.GetHttpContext()?.Request.Scheme}://{_aspNetUser.GetHttpContext()?.Request.Host}";
            var key = Encoding.ASCII.GetBytes(_token.Secret); //_jwksService.GetCurrent(); //NetDevPack.Security para jwt assimetrico publico e privado
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = currentIssuer,
                Audience = _token.ValidOn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(1), //para teste usar minutos
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature), //key
            });

            return tokenHandler.WriteToken(token);
        }

        private static LoginResponseViewModel GetResponseToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims, RefreshToken refreshToken)
        {
            return new LoginResponseViewModel()
            {
                AccessToken = encodedToken,
                RefreshToken = refreshToken.Token,
                ExpiresIn = TimeSpan.FromHours(1).TotalSeconds, //para teste usar minutos
                UserToken = new UserTokenViewModel()
                {
                    Id = user.Id,
                    Email = user.Email ?? "",
                    Claims = claims.Select(c => new ClaimViewModel() { Type = c.Type, Value = c.Value })
                }
            };
        }

        private async Task<RefreshToken> GenerateRefreshTokenAsync(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var refreshToken = new RefreshToken(email, DateTime.UtcNow.AddHours(_token.RefreshTokenExpirationHours));

            await _refreshTokenRepository.DeleteRefreshTokenAsync(email, cancellationToken).ConfigureAwait(false);
            await _refreshTokenRepository.CreateRefreshTokenAsync(refreshToken, cancellationToken).ConfigureAwait(false);
            await _refreshTokenRepository.CommitAsync().ConfigureAwait(false);

            return refreshToken;
        }

        private static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}