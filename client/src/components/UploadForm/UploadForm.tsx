type Props = {
    file: File | null
    loading: boolean
    error: string | null
    onFileChange: (e: React.ChangeEvent<HTMLInputElement>) => void
    onSubmit: () => void
  }
  
  const UploadForm = ({ file, loading, error, onFileChange, onSubmit }: Props) => {
    return (
      <div className="max-w-xl mx-auto p-6 border-2 border-dashed rounded-lg mb-10">
        <input type="file" accept=".txt" onChange={onFileChange} className="block w-full mb-4 text-sm" />
        <button
          onClick={onSubmit}
          disabled={!file || loading}
          className="w-full bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition disabled:opacity-50"
        >
          {loading ? "Uploading..." : "Submit"}
        </button>
        {error && <p className="text-red-500 text-sm mt-3">{error}</p>}
      </div>
    )
  }
  
  export default UploadForm
  