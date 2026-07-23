import "./globals.css";
import Navbar from "./components/Navbar";


export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className="min-h-full flex flex-col">
        <Navbar></Navbar>
         <main className="">
        {children}
        </main>
      </body>
    </html>
  );
}
