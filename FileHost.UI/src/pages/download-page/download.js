import React, { useState, useEffect, useRef } from "react";
import './download.css';
import {useFetching} from "../../hooks/useFetching";
import PostService from "../../api/PostService";
import { useSearchParams } from "react-router-dom";
import {Card} from "../../components/card";
import { Error } from "../../components/error";

export const DownloadPage = () => {
    const [searchParams, setSearchParams] = useSearchParams();
    const success = useRef(false);
    const message = useRef("");
    const [fileData, setFileData] = useState([]);
    const [fetchPost, postError] = useFetching(async() => {
        const response = await PostService.GetFile(window.location.pathname.replace('/',''));
        setFileData(response.data.data);
        success.current = response.data.success;
        message.current = response.data.message;
    });

    useEffect(() => {
        fetchPost();
      }, []);
   

    return(
        <div>
           {success.current ? (<Card model={fileData}/>) : (<Error message = {message.current}/>) }
        </div>
    )
}