import '@/app/ui/global.css';
import { source } from '@/app/ui/fonts';
import { ThemeProvider, theme} from '@primer/react';
export default async function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <ThemeProvider dayScheme="dark" nightScheme="dark" theme={theme}>
        <body className={`${source.className} antialiased`}>{children}</body>
      </ThemeProvider>
    </html>
  );
}
