import React, { useState, useEffect, useRef } from "react";
import {useFetching} from "../../hooks/useFetching";
import PostService from "../../api/PostService";
import './home.css';

export const HomePage = () => {
    const initial = {FileData: null};
    const fileRef = useRef(initial);

    const [fetchPost, postError] = useFetching(async() => {
        const response = await PostService.PostFile(fileRef.current);
        window.location.href = `http://localhost:3000/${response.data.data.id}`;
    });

    function handleChangeFile(e) {
        e.preventDefault();
        let file = e.target.files[0];
        let reader = new FileReader();

        reader.onload = function(e) {
            initial.FileData = fileRef.current;
        }
            fileRef.current = file;
           
             fetchPost();
      
       reader.readAsArrayBuffer(file);
    }


    return(
        <div>
            <input type="file" id="input__file"  onChange={handleChangeFile} style={{display:"none"}}></input>
            <label for="input__file"  className='btn'>
                <span>Загрузить</span>
             </label>
        </div>
    )
}