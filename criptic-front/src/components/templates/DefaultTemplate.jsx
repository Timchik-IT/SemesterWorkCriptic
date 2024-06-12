import Header from "../Header";

const DefaultTemplate = ({ children }) => {
    return (
        <div className="bg-gray-900 text-white min-h-screen">
            <Header />
            <div className="p-8">{children}</div>
        </div>
    )
}
  
export default DefaultTemplate;