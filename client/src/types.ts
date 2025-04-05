export type Holding = {
    coin: string
    amount: number
    buyPrice: number
    currentPrice: number
    totalPaid: number
    currentValue: number
    winLoss: number
    winLossPercentage: number
  }
  
  export type PortfolioResult = {
    initialValue: number
    currentValue: number
    totalWinLoss: number
    totalWinLossPercentage: number
    holdings: Holding[]
  }
  