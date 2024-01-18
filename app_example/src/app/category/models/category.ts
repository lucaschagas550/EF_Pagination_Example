import { CategoryProduct } from "./category-product";

export interface Category {
    id: string;
    name: string;
    description: string;
    categoryProduct: CategoryProduct[];
}