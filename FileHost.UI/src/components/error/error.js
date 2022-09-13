import React from "react";
import './error.css';
import {BiMessageRoundedError} from 'react-icons/bi';

export const Error = (message) => {
    return(
        <div class="audun_warn">
            <BiMessageRoundedError/>.
             {message.message}
        </div>
    )
}