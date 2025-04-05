import { PortfolioResult } from "../types"

const PortfolioSummary = ({ data }: { data: PortfolioResult }) => {
  return (
    <div className="text-center mt-6 space-y-1 text-sm sm:text-base">
      <p><i className="bx bx-wallet mr-1" /> <strong>Initial:</strong> ${data.initialValue.toFixed(2)}</p>
      <p><i className="bx bx-bar-chart-square" /> <strong>Current:</strong> ${data.currentValue.toFixed(2)}</p>
      <p className="flex justify-center items-center gap-2">
        <i className={`bx ${data.totalWinLoss >= 0 ? 'bx-trending-up text-green-500' : 'bx-trending-down text-red-500'}`} />
        <strong className={`${data.totalWinLoss >= 0 ? 'text-green-600' : 'text-red-500'}`}>
          {data.totalWinLoss >= 0 ? "Profit" : "Loss"}:
          ${data.totalWinLoss.toFixed(2)} ({data.totalWinLossPercentage.toFixed(2)}%)
        </strong>
      </p>
    </div>
  )
}

export default PortfolioSummary
