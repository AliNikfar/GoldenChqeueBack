export interface BaseModel {
  id: string;
  registerDate?: string;
  lastChangeDate?: string;
  visable?: boolean;
}

export interface State extends BaseModel { name: string; }

export interface City extends BaseModel { name: string; cityCode: string; ostan: string; }

export interface CustomerRate extends BaseModel { title: string; }

export interface Customer extends BaseModel {
  code: number;
  name: string;
  lastName: string;
  fatherName?: string;
  phoneNum: string;
  mob1?: string;
  mob2?: string;
  mob3?: string;
  city: string;
  address?: string;
  postalCode?: string;
  details?: string;
  maxBuyPrice?: number;
  birthDate?: string;
  customerRate: string;
}

export interface Unit extends BaseModel { name: string; quantityPerUnit: number; }

export interface Category extends BaseModel { title: string; parentId?: string | null; }

export interface Bank extends BaseModel { title: string; shobeList?: Shobe[]; }

export interface Shobe extends BaseModel {
  name: string;
  code: string;
  phone?: string;
  address?: string;
  details?: string;
}

export interface Product extends BaseModel {
  title: string;
  price: number;
  buyPrice: number;
  unit?: Unit | null;
  category?: Category | null;
  wareHouseStock: number;
  image?: { url?: string } | null;
}

export interface Factor extends BaseModel {
  personCode: string;
  factorSumPrice: number;
  factorSodDarsad: number;
  factorKharidDate: string;
  factorSumObjectsPrice: number;
  kind: number;
  factorBeforePrice: number;
}

export interface Ghest extends BaseModel {
  price: number;
  status: boolean;
  date: string;
  passDate: string;
  factor: string;
}

export interface Cheque extends BaseModel {
  kind: number;
  shomareHesab: number;
  shomareChek: number;
  sahabCheque: string;
  shobe: string;
  chequeDate: string;
  chequeStatus: number;
  passDate: string;
  detail?: string;
  factorID: string;
  chequePrice: number;
}
