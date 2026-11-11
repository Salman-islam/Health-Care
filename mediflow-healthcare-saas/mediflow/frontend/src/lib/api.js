const base=import.meta.env.VITE_API_URL||'http://localhost:5000/api';
export async function api(path,options={}){const token=sessionStorage.getItem('token');const response=await fetch(`${base}${path}`,{...options,headers:{'Content-Type':'application/json',...(token?{Authorization:`Bearer ${token}`}:{}) ,...options.headers}});if(!response.ok){let message=await response.text();throw new Error(message||`HTTP ${response.status}`)}if(response.status===204)return null;return response.json()}
export const save=(path,data,method='POST')=>api(path,{method,body:JSON.stringify(data)});
