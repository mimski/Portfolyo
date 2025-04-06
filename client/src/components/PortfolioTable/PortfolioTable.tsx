import { PortfolioResult } from "../../types"

const PortfolioTable = ({ portfolio }: { portfolio: PortfolioResult }) => {
  if (!portfolio || !Array.isArray(portfolio.holdings)) {
    return <p className="text-red-500">No portfolio data available.</p>
  }

  return (
    <div className="overflow-auto rounded-md shadow-sm border border-gray-300 dark:border-gray-700 mt-6">
      <table className="min-w-full table-auto text-sm">
        <thead className="bg-gray-100 dark:bg-gray-700">
          <tr>
            {[
              "#",
              "Coin",
              "Amount",
              "Buy Price",
              "Total Paid",
              "Current Price",
              "Current Value",
              "Win/Loss",
              "% Change",
            ].map((head, i) => (
              <th
                key={i}
                className="px-3 py-2 border dark:border-gray-600 text-left font-semibold"
              >
                {head}
              </th>
            ))}
          </tr>
        </thead>
        <tbody>
          {portfolio.holdings.map((h, i) => (
            <tr
              key={i}
              className="odd:bg-white even:bg-gray-50 dark:odd:bg-gray-800 dark:even:bg-gray-900"
            >
              <td className="px-3 py-2 border">{i + 1}</td>
              <td className="px-3 py-2 border">{h.coin ?? "N/A"}</td>
              <td className="px-3 py-2 border">{h.amount ?? 0}</td>
              <td className="px-3 py-2 border">
                ${h.buyPrice?.toFixed(2) ?? "0.00"}
              </td>
              <td className="px-3 py-2 border">
                ${h.totalPaid?.toFixed(2) ?? "0.00"}
              </td>
              <td className="px-3 py-2 border">
                ${h.currentPrice?.toFixed(2) ?? "0.00"}
              </td>
              <td className="px-3 py-2 border">
                ${h.currentValue?.toFixed(2) ?? "0.00"}
              </td>
              <td
                className={`px-3 py-2 border ${h.winLoss >= 0 ? "text-green-600" : "text-red-500"
                  }`}
              >
                ${h.winLoss?.toFixed(2) ?? "0.00"}
              </td>
              <td className="px-3 py-2 border">
                {h.winLossPercentage?.toFixed(2) ?? "0.00"}%
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}

export default PortfolioTable
