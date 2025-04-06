import { useState } from "react"
import UploadForm from "./UploadForm/UploadForm"
import PortfolioTable from "./PortfolioTable/PortfolioTable"
import PortfolioSummary from "./PortfolioSummary/PortfolioSummary"
import { PortfolioResult } from "../types"
import LastUpdatedInfo from "./LastUpdatedInfo"

const AppMain = () => {
  const [file, setFile] = useState<File | null>(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [portfolio, setPortfolio] = useState<PortfolioResult | null>(null)

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
    }
    catch (err) {
      if (err instanceof Error) {
        setError(err.message)
      } else {
        setError("Something went wrong")
      }
    }
    finally {
      setLoading(false)
    }
  }

  return (
    <>
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
              <i className="bx bx-reset"></i> Restart
            </button>
            <p className="text-green-500 font-medium flex items-center gap-1">
              <i className="bx bx-check-circle text-lg" /> Portfolio successfully analyzed
            </p>
          </div>
          {portfolio?.lastUpdated && (
            <div className="text-left text-sm text-gray-500 dark:text-gray-400 mb-3">
              <LastUpdatedInfo timestamp={portfolio.lastUpdated} />
            </div>
          )}
          <PortfolioTable portfolio={portfolio} />
          <PortfolioSummary data={portfolio} />
        </>
      )}
    </>
  )
}

export default AppMain
