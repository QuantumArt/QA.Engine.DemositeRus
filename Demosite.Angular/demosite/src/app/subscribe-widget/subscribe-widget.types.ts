export interface NewsCategoriesQueryResult {
  newsCategories: {
    items: {
      id: number;
      alternativeTitle: string;
      alias: string;
      sortOrder: number;
    }[];
  };
}

export interface NewsCategory {
  id: number;
  alias: string;
  title: string;
  sortOrder: number;
}

export interface SubscribeRequest {
  company: string;
  email: string;
  firstName: string;
  gender: string;
  lastName: string;
  categories: boolean[];
}

export interface SubscribeRequestResult {
  succeed: boolean;
  error?: string;
}
