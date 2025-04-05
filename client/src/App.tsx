import { useState, useEffect } from "react"
import Header from "./components/Header"
import UploadForm from "./components/UploadForm"
import PortfolioTable from "./components/PortfolioTable"
import PortfolioSummary from "./components/PortfolioSummary"
import { PortfolioResult } from "./types"

const App = () => {
  const [darkMode, setDarkMode] = useState(false)
  const [file, setFile] = useState<File | null>(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [portfolio, setPortfolio] = useState<PortfolioResult | null>(null)

  useEffect(() => {
    document.documentElement.classList.toggle("dark", darkMode)
  }, [darkMode])

  const handleSubmit = async () => {
    if (!file) return
    setLoading(true)
    setError(null)

    const formData = new FormData()
    formData.append("file", file)

    try {
      const response = await fetch("https://localhost:5001/api/portfolio/upload", {
        method: "POST",
        body: formData,
      })

      if (!response.ok) throw new Error(await response.text())
      const result: PortfolioResult = await response.json()
      setPortfolio(result)
    } catch (err: any) {
      setError(err.message || "Something went wrong")
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="min-h-screen bg-white dark:bg-gray-900 text-gray-900 dark:text-white transition-colors duration-300">
      <Header darkMode={darkMode} toggleDarkMode={() => setDarkMode(!darkMode)} />
      <main className="p-6 max-w-6xl mx-auto">
        <h2 className="text-3xl font-semibold text-center mb-6">Crypto Portfolio</h2>

        {!portfolio && (
          <UploadForm
            file={file}
            loading={loading}
            error={error}
            onFileChange={(e) => setFile(e.target.files?.[0] || null)}
            onSubmit={handleSubmit}
          />
        )}

        {portfolio && (
          <>
            <div className="flex flex-col sm:flex-row justify-between items-center gap-4 mb-4">
              <button
                onClick={() => {
                  setFile(null)
                  setPortfolio(null)
                  setError(null)
                }}
                className="bg-gray-500 text-white px-4 py-2 rounded hover:bg-gray-600 transition"
              >
                <i className='bx bx-reset'></i> Restart
              </button>
              <p className="text-green-500 font-medium flex items-center gap-1">
                <i className="bx bx-check-circle text-lg" /> Portfolio successfully analyzed
              </p>
            </div>

            <PortfolioTable portfolio={portfolio} />
            <PortfolioSummary data={portfolio} />
          </>
        )}
      </main>
    </div>
  )
}

export default App
