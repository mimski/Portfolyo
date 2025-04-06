import { PortfolioResult } from "../../types"

const PortfolioSummary = ({ data }: { data: PortfolioResult }) => {
  if (!data) {
    return <p className="text-red-500">Invalid portfolio data.</p>
  }

  const {
    initialValue,
    currentValue,
    totalWinLoss,
    totalWinLossPercentage,
  } = data

  const isProfit = totalWinLoss >= 0
  const color = isProfit ? "text-green-600" : "text-red-500"
  const trendIcon = isProfit ? "bx-trending-up text-green-500" : "bx-trending-down text-red-500"
  const label = isProfit ? "Profit" : "Loss"

  return (
    <div className="text-center mt-6 space-y-1 text-sm sm:text-base">
      <p>
        <i className="bx bx-wallet mr-1" />
        <strong>Initial:</strong> ${initialValue?.toFixed(2) ?? "0.00"}
      </p>
      <p>
        <i className="bx bx-bar-chart-square" />
        <strong>Current:</strong> ${currentValue?.toFixed(2) ?? "0.00"}
      </p>
      <p className="flex justify-center items-center gap-2">
        <i className={`bx ${trendIcon}`} />
        <strong className={color}>
          {label}: ${totalWinLoss?.toFixed(2) ?? "0.00"} (
          {totalWinLossPercentage?.toFixed(2) ?? "0.00"}%)
        </strong>
      </p>
    </div>
  )
}

export default PortfolioSummary
