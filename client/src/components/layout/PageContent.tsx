import Header from "./Header"
import Footer from "./Footer"
import { ReactNode } from "react"

interface PageContentProps {
  children: ReactNode
  darkMode: boolean
  toggleDarkMode: () => void
}

const PageContent = ({ children, darkMode, toggleDarkMode }: PageContentProps) => {
  return (
    <div className="min-h-screen bg-white dark:bg-gray-900 text-gray-900 dark:text-white transition-colors duration-300 flex flex-col">
      <Header darkMode={darkMode} toggleDarkMode={toggleDarkMode} />
      <main className="flex-1 p-6 mx-auto">{children}</main>
      <Footer />
    </div>
  )
}

export default PageContent
