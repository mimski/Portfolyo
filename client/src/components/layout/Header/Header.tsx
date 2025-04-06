type Props = {
  darkMode: boolean
  toggleDarkMode: () => void
}

const Header = ({ darkMode, toggleDarkMode }: Props) => {
  return (
    <header className="p-4 flex justify-between items-center border-b border-gray-200 dark:border-gray-700 transition-colors duration-300">
      <h1 className="text-xl font-bold flex items-center gap-2">
        <i className="bx bxl-bitcoin text-yellow-500 text-2xl"></i> Portfolyo
      </h1>

      <button
        onClick={toggleDarkMode}
        className="flex items-center gap-2 px-4 py-2 text-sm border rounded border-gray-400 dark:border-gray-600 hover:bg-gray-100 dark:hover:bg-gray-800 transition"
        aria-label={`Switch to ${darkMode ? "Light" : "Dark"} Mode`}
      >
        <i className={`bx bx-${darkMode ? "sun" : "moon"} text-lg`}></i>
        <span>{`Switch to ${darkMode ? "Light" : "Dark"} Mode`}</span>
      </button>
    </header>
  )
}

export default Header
