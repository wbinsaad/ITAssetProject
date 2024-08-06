
async function GetAssets() {
    const req = await fetch('/api/Asset', {
        method: "GET",
    });

    const AssetsList = await req.json();

    return AssetsList;
}

async function GetAsset(Id) {
    if (Id == null) return;

    const req = await fetch('/api/Asset/' + Id, {
        method: "GET"
    });

    const Asset = await req.json();

    return Asset;
}

const AddAsset = async (Asset) => {
    const req = await fetch('/api/Asset', {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(Asset)
    });

    const json = await req.json();

    return { ok : req.ok, responce : json };
}

const UpdateAsset = async (Id, Asset) => {
    const req = await fetch('/api/Asset/' + Id, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(Asset)
    });
}

const DeleteAsset = async (Id) => {
    const req = await fetch('/api/Asset/' + Id, {
        method: "DELETE"
    });
}