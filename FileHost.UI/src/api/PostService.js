import axios from 'axios';

export default class PostService {
    static async PostFile(model) {
        const formData = new FormData();

        try {
            formData.append("file", model);
            const response = await axios.post("https://localhost:5001/api/File", formData);
            return response;
        }
        catch(e) {
            return(e.response);
        }
    }

    static async GetFile(id) {
        try {
            const response = await axios.get(`https://localhost:5001/api/File/${id}`);
            return response;
        }
        catch(e) {
            return(e.response);
        }
    }
}

