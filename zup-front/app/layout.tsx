import '@/app/ui/global.css';
import { source } from '@/app/ui/fonts';
import { SessionProvider } from 'next-auth/react';
import { authOptions } from './api/auth/[...nextauth]/route';
import getServerSession from 'next-auth';
export default async function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const session = getServerSession(authOptions);
  return (
    <SessionProvider session={session}>
      <html lang="en">
        <body className={`${source.className} antialiased`}>{children}</body>
      </html>
    </SessionProvider>
  );
}
