import { render, screen } from "@testing-library/react"
import Footer from "./Footer"

describe("Footer", () => {
  it("renders copyright text", () => {
    render(<Footer />)
    const year = new Date().getFullYear()
    expect(screen.getByText(`© ${year} Portfolyo - Crypto Portfolio. All rights reserved.`)).toBeInTheDocument()
  })
})
