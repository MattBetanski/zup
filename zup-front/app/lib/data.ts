import { sql } from '@vercel/postgres';
import {
  CustomerField,
  CustomersTableType,
  InvoiceForm,
  InvoicesTable,
  LatestInvoiceRaw,
  User,
  Revenue,
  Invoice,
  LatestInvoice
} from './definitions';
import { formatCurrency } from './utils';
import { unstable_noStore as noStore } from 'next/cache';

export async function fetchRevenue() {
  // Add noStore() here to prevent the response from being cached.
  // This is equivalent to in fetch(..., {cache: 'no-store'}).
  noStore();
  try {
    const response = await fetch('http://localhost:9090/revenue');
    const json_data = await response.json();
    const revenue: Revenue[] = json_data;
    return revenue;
  } catch (error) {
    console.error('Database Error:', error);
    throw new Error('Failed to fetch revenue data.');
  }
}

export async function fetchLatestInvoices() {
  noStore();
  try {

    const response = await fetch('http://localhost:9090/invoices/latest');
    const invoices = await response.json();
    const invoices_formatted: LatestInvoice[] = invoices.map((invoice: {
      email: string;
      imageUrl: string;
      name: string;
      amount: number;
      id: string; 
    }) => (
      {
        amount: formatCurrency(invoice.amount),
        id: invoice.id,
        name: invoice.name,
        image_url: invoice.imageUrl,
        email: invoice.email,
      }
    ));
    return invoices_formatted;
  } catch (error) {
    console.error('Database Error:', error);
    throw new Error('Failed to fetch the latest invoices.');
  }
}

export async function fetchCardData() {
  noStore();
  try {
    const sleep = (ms: number | undefined) => new Promise(r => setTimeout(r, ms));
    //await sleep(100000000);
    const response = await fetch('http://localhost:9090/dashboard/cards');
    const json_data = await response.json();
    const numberOfInvoices = json_data.numberOfInvoices ?? '0';
    const numberOfCustomers = json_data.numberOfCustomers ?? '0';
    const totalPaidInvoices = formatCurrency(json_data.paidInvoices ?? '0');
    const totalPendingInvoices = formatCurrency(json_data.pendingInvoices ?? '0');
    // You can probably combine these into a single SQL query
    // However, we are intentionally splitting them to demonstrate
    // how to initialize multiple queries in parallel with JS.


    //const totalPaidInvoices = formatCurrency(data[2].rows[0].paid ?? '0');
    //const totalPendingInvoices = formatCurrency(data[2].rows[0].pending ?? '0');

    return {
      numberOfCustomers,
      numberOfInvoices,
      totalPaidInvoices,
      totalPendingInvoices,
    };
  } catch (error) {
    console.error('Database Error:', error);
    throw new Error('Failed to fetch card data.');
  }
}

const ITEMS_PER_PAGE = 6;
export async function fetchFilteredInvoices(
  query: string,
  currentPage: number,
) {
  noStore();
  const offset = (currentPage - 1) * ITEMS_PER_PAGE;
  try {
    const response = await fetch(`http://localhost:9090/invoices?query=${query}&offset=${offset}`);
    const json_data: InvoicesTable[] = await response.json();
    return json_data;
    const invoices = await sql<InvoicesTable>`
      SELECT
        invoices.id,
        invoices.amount,
        invoices.date,
        invoices.status,
        customers.name,
        customers.email,
        customers.image_url
      FROM invoices
      JOIN customers ON invoices.customer_id = customers.id
      WHERE
        customers.name ILIKE ${`%${query}%`} OR
        customers.email ILIKE ${`%${query}%`} OR
        invoices.amount::text ILIKE ${`%${query}%`} OR
        invoices.date::text ILIKE ${`%${query}%`} OR
        invoices.status ILIKE ${`%${query}%`}
      ORDER BY invoices.date DESC
      LIMIT ${ITEMS_PER_PAGE} OFFSET ${offset}
    `;

    return invoices.rows;
  } catch (error) {
    console.error('Database Error:', error);
    throw new Error('Failed to fetch invoices.');
  }
}

export async function fetchInvoicesPages(query: string) {
  noStore();
  try {
    const response = await fetch(`http://localhost:9090/invoices/count?query=${query}`);
    const json_data = await response.json();
    const page_count = json_data.count;
    const total_count = Math.ceil(Number(page_count / ITEMS_PER_PAGE));
    return total_count;
  } catch (error) {
    console.error('Database Error:', error);
    throw new Error('Failed to fetch total number of invoices.');
  }
}

export async function fetchInvoiceById(id: string) {
  try {
    const response = await fetch(`http://localhost:9090/invoices/${id}`);
    if(response.status == 404) {
      return null;
    }
    const json_data: Invoice = await response.json();
    json_data.amount = json_data.amount / 100;
    return json_data;
    const data = await sql<InvoiceForm>`
      SELECT
        invoices.id,
        invoices.customer_id,
        invoices.amount,
        invoices.status
      FROM invoices
      WHERE invoices.id = ${id};
    `;

    const invoice = data.rows.map((invoice) => ({
      ...invoice,
      // Convert amount from cents to dollars
      amount: invoice.amount / 100,
    }));

    return invoice[0];
  } catch (error) {
    console.error('Database Error:', error);
    throw new Error('Failed to fetch invoice.');
  }
}

export async function fetchCustomers() {
  try {
    const response = await fetch('http://localhost:9090/customers');
    const customer_data: CustomerField[] = await response.json();
    return customer_data;
    const data = await sql<CustomerField>`
      SELECT
        id,
        name
      FROM customers
      ORDER BY name ASC
    `;

    const customers = data.rows;
    return customers;
  } catch (err) {
    console.error('Database Error:', err);
    throw new Error('Failed to fetch all customers.');
  }
}

export async function fetchFilteredCustomers(query: string) {
  try {
    const data = await sql<CustomersTableType>`
		SELECT
		  customers.id,
		  customers.name,
		  customers.email,
		  customers.image_url,
		  COUNT(invoices.id) AS total_invoices,
		  SUM(CASE WHEN invoices.status = 'pending' THEN invoices.amount ELSE 0 END) AS total_pending,
		  SUM(CASE WHEN invoices.status = 'paid' THEN invoices.amount ELSE 0 END) AS total_paid
		FROM customers
		LEFT JOIN invoices ON customers.id = invoices.customer_id
		WHERE
		  customers.name ILIKE ${`%${query}%`} OR
        customers.email ILIKE ${`%${query}%`}
		GROUP BY customers.id, customers.name, customers.email, customers.image_url
		ORDER BY customers.name ASC
	  `;

    const customers = data.rows.map((customer) => ({
      ...customer,
      total_pending: formatCurrency(customer.total_pending),
      total_paid: formatCurrency(customer.total_paid),
    }));

    return customers;
  } catch (err) {
    console.error('Database Error:', err);
    throw new Error('Failed to fetch customer table.');
  }
}

export async function getUser(email: string) {
  try {
    const user = await sql`SELECT * FROM users WHERE email=${email}`;
    return user.rows[0] as User;
  } catch (error) {
    console.error('Failed to fetch user:', error);
    throw new Error('Failed to fetch user.');
  }
}
export default async function Page({params}: {params: {id: string}}) {
  const id = params.id;
  
  
}

export async function fetchDepartmentbyId(id: string) {
  try {
    if (id == "1") {
      return {
        id: 1,
        name: "Test Name",
        description: "Test Description",
        creationDate: Date.now(),
        visbility: "public"
      }
    } else {
      return null;
    }
  } catch (err) {
    console.error("Error:", err);
    throw err;
  }
}

export async function fetchProjectsForDepartment(deparmtentId: string) {
  try {
    return [
      {
        "projectId": 1,
        "departmentId": 1,
        "name": "Test Project One",
        "description": "This is a test project",
        "createdDate": Date.now()
      },
      {
        "projectId": 2,
        "departmentId": 1,
        "name": "Test Project Two",
        "description": "Another test project",
        "createdDate": Date.now()
      },
      {
        "projectId": 3,
        "departmentId": 1,
        "name": "Test Project Three",
        "description": " The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment",
        "createdDate": Date.now()
      },
      {
        "projectId": 3,
        "departmentId": 1,
        "name": "Test Project Three",
        "description": " The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment",
        "createdDate": Date.now()
      },
      {
        "projectId": 3,
        "departmentId": 1,
        "name": "Test Project Three",
        "description": " The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment",
        "createdDate": Date.now()
      },
      {
        "projectId": 3,
        "departmentId": 1,
        "name": "Test Project Three",
        "description": " The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment",
        "createdDate": Date.now()
      },
      {
        "projectId": 3,
        "departmentId": 1,
        "name": "Test Project Three",
        "description": " The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment The third test project for this deparment",
        "createdDate": Date.now()
      }
    ]
  } catch (err) {
    console.error("Error:", err);
    throw err;
  }
}

export async function fetchWikiPagesForDepartment(departmentId: string) {
  try {
    return [
      {
        "wikiPageId": 1,
        "departmentId": 0,
        "title": "This is a wiki page",
        "filePath": "/test",
        "createdDate": Date.now()
      },
      {
        "wikiPageId": 2,
        "departmentId": 0,
        "title": "This is another wiki page",
        "filePath": "/test",
        "createdDate": Date.now()
      },
      {
        "wikiPageId": 3,
        "departmentId": 0,
        "title": "Yet another wiki page",
        "filePath": "/test",
        "createdDate": Date.now()
      },
    ]
  } catch (err) {

  }
}