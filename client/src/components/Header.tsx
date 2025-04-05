type Props = {
    darkMode: boolean
    toggleDarkMode: () => void
  }
  
  const Header = ({ darkMode, toggleDarkMode }: Props) => {
    return (
      <header className="p-4 flex justify-between items-center border-b border-gray-200 dark:border-gray-700">
        <h1 className="text-xl font-bold flex items-center gap-2">
          <i className="bx bxl-bitcoin"></i> Portfolyo
        </h1>
        <button
          onClick={toggleDarkMode}
          className="p-2 rounded border border-gray-400 dark:border-gray-600 flex justify-center items-center"
          aria-label="Toggle Theme"
        >
          <i className={`bx bx-${darkMode ? 'sun' : 'moon'} text-xl`}></i>
        </button>
      </header>
    )
  }
  
  export default Header
  