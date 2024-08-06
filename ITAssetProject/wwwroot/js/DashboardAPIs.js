
const GetAssetsByBrand = async () => {
    const req = await fetch('/api/Dashboard/AssetsByBrand');
    const json = await req.json();
    return json;
}

const GetAssetsByStatus = async () => {
    const req = await fetch('/api/Dashboard/AssetsByStatus');
    const json = await req.json();
    return json;
}