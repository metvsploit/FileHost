import React from "react";
import './card.css';
import {BiCopy} from 'react-icons/bi';



export const Card = (model) => {

    function Download(arrayBuffer, type) {
        const byteCharacters = atob(model.model.data);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], {type: model.model.format});
        var url = URL.createObjectURL(blob);
        let a = document.createElement('a');
	    a.href = url;
	    a.download = model.model.fileName;
	    a.click();
        window.URL.revokeObjectURL(url)
      }
    return(
        
      <div class="card">
        
            <div>
                <div className="card__input">
                    <i className="card__i"><BiCopy onClick={() =>  navigator.clipboard.writeText(window.location.href)} className="card__icons"/></i>
                    <input className="card__field" type="text" value={window.location.href}/>
                </div>
                <h2 className="card__title">{model.model.fileName}
                 </h2>
                <h4>Размер: {Math.round(model.model.size/1024/1024)} мб</h4>
                <h4>Формат: {model.model.format}</h4>
                <h4>Срок хранения: {model.model.storageTime.split('T')[0]}</h4>
            </div>
            
            <p className="card__apply">
                <a className="card__link" onClick={Download}>Скачать</a>
            </p>
      </div>
  
    )
}