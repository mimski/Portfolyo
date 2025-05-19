# Portfolyo
A responsive crypto portfolio visualization tool that integrates CoinLore API to calculate and monitor real-time portfolio performance from uploaded user data. Built with .NET and React.

**Portfolyo** is a full-stack cryptocurrency portfolio management app that lets users upload their crypto holdings in a `.txt` file and get real-time calculations of portfolio value, profit/loss, and performance using live data from [Coinlore API](https://www.coinlore.com/cryptocurrency-data-api).


## 🛠️ Tech Stack

| Layer        | Technologies                                             |
|--------------|----------------------------------------------------------|
| **Frontend** | React 19, TypeScript, Vite, Tailwind CSS                 |
| **Backend**  | .NET 9 Web API, Hosted Services, Serilog Logging         |

---

## 📦 Features

- Upload `.txt` file with crypto holdings: `AMOUNT|SYMBOL|BUY_PRICE`
- Fetches real-time coin prices from Coinlore API
- Calculates:
  - Initial portfolio value
  - Current portfolio value
  - Win/Loss and percentage change per coin
- Auto-refresh every 5 minutes (configurable)
- In-memory storage with thread-safe caching
- Logging with Serilog (to file and console)
- Light/Dark mode UI with Tailwind

---

## 📁 File Format (Upload)

Each line must follow the format:

```
X|COIN|Y
```

Example:
```
10|ETH|123.14
12.12454|BTC|24012.43
10000000|SHIB|60
1200|USDT|1123.23
```

---

## 🧑‍💻 Getting Started

### 🔹 Backend (.NET)

```bash
cd API
dotnet restore
dotnet run
```

API will run at: `https://localhost:5001`

### 🔹 Frontend (React)

```bash
cd client
npm install
npm run dev
```

Frontend will run at: `http://localhost:3000`

---

## ⚙️ Configuration

Located in `API/appsettings.json`:

```json
"CoinPrice": {
  "CacheMinutes": 1
},
"PortfolioRefresh": {
  "IntervalMinutes": 5
}
```

