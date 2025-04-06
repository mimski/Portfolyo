import { useState, useEffect } from "react"
import PageContent from "./components/layout/PageContent"
import AppMain from "./components/AppMain"

const App = () => {
  const [darkMode, setDarkMode] = useState(false)

  useEffect(() => {
    document.documentElement.classList.toggle("dark", darkMode)
  }, [darkMode])

  return (
    <PageContent
      darkMode={darkMode}
      toggleDarkMode={() => setDarkMode(!darkMode)}
    >
      <AppMain />
    </PageContent>
  )
}

export default App
