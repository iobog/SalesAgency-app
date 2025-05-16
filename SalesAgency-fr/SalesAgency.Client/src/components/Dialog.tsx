import ReactDOM from 'react-dom';

const Dialog = ({ isOpen, onClose, children } : any) => {
  if (!isOpen) return null;

  return ReactDOM.createPortal(
    <div className="fixed inset-0 bg-black/50 flex justify-center items-center z-50" onClick={onClose}>
      <div className="bg-white relative shadow-lg w-96" onClick={(e) => e.stopPropagation()}>
        {/* <button
          className="absolute top-4 right-6 text-2xl text-gray-600 hover:text-gray-800"
          onClick={onClose}
        >
          &times;
        </button> */}
        {children}
      </div>
    </div>,
    document.body
  );
};

export default Dialog;
