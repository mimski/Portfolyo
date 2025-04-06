type Props = {
  timestamp: string | null | undefined
  intervalMinutes?: number
}

const LastUpdatedInfo = ({ timestamp, intervalMinutes = 5 }: Props) => {
  if (!timestamp) return null

  const last = new Date(timestamp)
  const now = new Date()
  const diffMs = now.getTime() - last.getTime()
  const diffMinutes = Math.floor(diffMs / 60000)

  const formatted = new Intl.DateTimeFormat(undefined, {
    hour: "2-digit",
    minute: "2-digit",
  }).format(last)

  let color = "text-green-500"
  if (diffMinutes >= intervalMinutes * 2) color = "text-red-500"
  else if (diffMinutes >= intervalMinutes) color = "text-yellow-500"

  return (
    <p
      className={`font-medium ${color}`}
      title={`Last updated at ${last.toLocaleTimeString()}`}
    >
      <i className="bx bx-time"></i> Last updated: {formatted} ({diffMinutes} min ago)
    </p>
  )
}

export default LastUpdatedInfo
