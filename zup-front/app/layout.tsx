import '@/app/ui/global.css';
import { source } from '@/app/ui/fonts';
export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body className={`${source.className} antialiased`}>{children}</body>
    </html>
  );
}
