import { render, screen } from "@testing-library/react"
import Header from "./Header"

describe("Header", () => {
  it("renders the logo text", () => {
    render(<Header darkMode={false} toggleDarkMode={() => {}} />)
    expect(screen.getByText(/Portfolyo/i)).toBeInTheDocument()
  })

  it("renders the correct icon for light mode", () => {
    render(<Header darkMode={false} toggleDarkMode={() => {}} />)
    expect(screen.getByLabelText(/Switch to dark mode/i)).toBeInTheDocument()
    expect(screen.getByRole("button")).toContainHTML("bx-moon")
  })

  it("renders the correct icon for dark mode", () => {
    render(<Header darkMode={true} toggleDarkMode={() => {}} />)
    expect(screen.getByLabelText(/Switch to light mode/i)).toBeInTheDocument()
    expect(screen.getByRole("button")).toContainHTML("bx-sun")
  })
})
