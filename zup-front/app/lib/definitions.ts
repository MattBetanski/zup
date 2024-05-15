// This file contains type definitions for your data.
// It describes the shape of the data, and what data type each property should accept.
// For simplicity of teaching, we're manually defining these types.

import { DO_NOT_USE_OR_YOU_WILL_BE_FIRED_EXPERIMENTAL_REACT_NODES } from "react";
import internal from "stream";

// However, these types are generated automatically if you're using an ORM such as Prisma.
export type User = {
  id: string;
  name: string;
  email: string;
  password: string;
};

export type Customer = {
  id: string;
  name: string;
  email: string;
  image_url: string;
};

export type Invoice = {
  id: string;
  customer_id: string;
  amount: number;
  date: string;
  // In TypeScript, this is called a string union type.
  // It means that the "status" property can only be one of the two strings: 'pending' or 'paid'.
  status: 'pending' | 'paid';
};

export type Revenue = {
  month: string;
  revenue: number;
};

export type LatestInvoice = {
  id: string;
  name: string;
  image_url: string;
  email: string;
  amount: string;
};

// The database returns a number for amount, but we later format it to a string with the formatCurrency function
export type LatestInvoiceRaw = Omit<LatestInvoice, 'amount'> & {
  amount: number;
};

export type InvoicesTable = {
  id: string;
  customer_id: string;
  name: string;
  email: string;
  image_url: string;
  date: string;
  amount: number;
  status: 'pending' | 'paid';
};

export type CustomersTableType = {
  id: string;
  name: string;
  email: string;
  image_url: string;
  total_invoices: number;
  total_pending: number;
  total_paid: number;
};

export type MemberTableType = {
  id: string;
  name: string;
  email: string;
  role: number;
  hire_date: string;
};

export type RoleTableType ={
  id: string;
  name: string;
  description: string;
  permissions: string;
}
export type FormattedCustomersTable = {
  id: string;
  name: string;
  email: string;
  image_url: string;
  total_invoices: number;
  total_pending: string;
  total_paid: string;
};

export type CustomerField = {
  id: string;
  name: string;
};

export type InvoiceForm = {
  id: string;
  customer_id: string;
  amount: number;
  status: 'pending' | 'paid';
};


export type Department = {
  ownerId: number;
  name: string;
  description: string;
  departmentId: number;
  creationDate: Date;
  visibility: boolean;
}

export type Item = {
  itemId: number;
  projectId: number;
  ownerId: number;
  parentId: number | null;
  name: string;
  description: string;
  type: ItemType;
  createdDate: Date;
  state: ItemState;
}

export enum ItemState {
  Open = "Open",
  InProgress = "InProgress",
  Completed = "Completed",
  Blocked = "Blocked"
}

export enum ItemType {
  Epic = "Epic",
  Feature = "Feature",
  Requirement = "Requirement",
  Task = "Task",
  Bug = "Bug"
}

export type Project = {
  projectId: number;
  departmentId: number;
  name: string;
  description: string;
  createdDate: string;
}

export enum PermissionLevel {
  NoAccess = "No Access",
  Read = "Read",
  Modify = "Modify"
}
export type Role = {
  roleId: number;
  departmentId: number;
  name: string;
  description: string;
  itemLevel: PermissionLevel;
  wikiLevel: PermissionLevel;
  wikiDelete: boolean;
}

export type UserRole = {
  email: string;
  firstName: string;
  lastName: string;
  roleId: number;
  roleName: string;
  userId: number;
}

export type WikiListing = {
  wikiId: number;
  departmentId: number;
  title: string;
  createDate: Date;
}

export type WikiPage = {
  wikiPageId: number;
  departmentId: number;
  title: string;
  createDate: Date;
  content: string;
}

export type Note = {
  noteId: number;
  ownerId: number;
  title: string;
  content: string;
  createDate: Date;
  likeCount: number;
  departmentId: number;
  projectId: number;
}