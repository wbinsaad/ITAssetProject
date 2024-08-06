let gridApi;

window.addEventListener('DOMContentLoaded', () => {
    LoadContent();

    $('#AddModalForm').on('submit', (e) => {
        e.preventDefault();
        const SerialNumber = $(e.target).find('[name="SerialNumber"]:first').val();
        const Brand = $(e.target).find('[name="Brand"]:first').val();
        const WarrantyExpirationDate = $(e.target).find('[name="WarrantyExpirationDate"]:first').val();
        const Status = $(e.target).find('[name="Status"]:first').val();

        AddAssetRow(SerialNumber, Brand, WarrantyExpirationDate, Status);
    });

    $('#EditModalForm').on('submit', (e) => {
        e.preventDefault();
        const id = $(e.target).find('[name="id"]:first').val();
        const serialNumber = $(e.target).find('[name="SerialNumber"]:first').val();
        const brand = $(e.target).find('[name="Brand"]:first').val();
        const warrantyExpirationDate = $(e.target).find('[name="WarrantyExpirationDate"]:first').val();
        const status = $(e.target).find('[name="Status"]:first').val();

        EditAsset(id, { id, serialNumber, brand, warrantyExpirationDate, status });

        const selectedRows = gridApi.getSelectedRows();
        const selectedRow = selectedRows.pop();

        selectedRow.serialNumber = serialNumber;
        selectedRow.brand = brand;
        selectedRow.warrantyExpirationDate = warrantyExpirationDate;
        selectedRow.status = status;

        gridApi.applyTransaction({ update: [selectedRow] });

        $('#EditModal').modal('hide');
    });


    $('#EditModal').on('show.bs.modal', () => ShowEditAssetModel());
});



const LoadContent = async () => {
    const AssetsList = await GetAssets();

    const gridOptions = {
        pagination: true,
        paginationPageSize: 500,
        paginationPageSizeSelector: [200, 500, 1000],

        rowSelection: "multiple",

        // OnCell value changed
        onCellValueChanged: (event) => EditAsset(event.data.id, event.data),

        // Row Data: The data to be displayed.
        rowData: AssetsList,
        // Column Definitions: Defines the columns to be displayed.
        columnDefs: [
            {
                headerName: "Serial Number",
                field: "serialNumber",
                cellDataType: "number",
                editable: true,
                filter: true,
                flex: 1,
                headerCheckboxSelection: true,
                checkboxSelection: true,

                valueSetter: (params) => {
                    return params.data.serialNumber.toString();
                },
                valueGetter: (params) => {
                    if (params.data.serialNumber) {
                        return Number(params.data.serialNumber);
                    } else {
                        return undefined;
                    }
                },
            },
            {
                headerName: "Brand",
                field: "brand",
                cellDataType: "text",
                editable: true,
                filter: true,
                flex: 1
            },
            {
                headerName: "Status",
                field: "status",
                cellEditor: 'agSelectCellEditor',
                editable: true,
                editable: true,
                flex: 1,
                cellEditorParams: {
                    values: ['New', 'InUse', 'Damaged', 'Dispose'],
                }
            },
            {
                headerName: "Warranty Expiration Date",
                field: "warrantyExpirationDate",
                cellDataType: "date",
                editable: true,
                filter: true,
                valueSetter: (params) => {
                    const newVal = params.newValue;
                    params.data.warrantyExpirationDate = newVal;
                    return params.data.warrantyExpirationDate;
                },
                valueGetter: (params) => {
                    if (params.data.warrantyExpirationDate) {
                        return new Date(params.data.warrantyExpirationDate);
                    } else {
                        return undefined;
                    }
                },
            }

        ]
    };

    const myGridElement = document.querySelector('#myGrid');
    gridApi = agGrid.createGrid(myGridElement, gridOptions);
}

const EditAsset = async (Id, Asset) => {
    const req = await UpdateAsset(Id, Asset);

}

const ShowEditAssetModel = async () => {
    const selectedRows = gridApi.getSelectedRows();

    if (!selectedRows) {
        return;
    }

    // Incase the user didn't selected any row
    if (selectedRows == 0) {
        return;
    }

    // Incase the user selected more then one row
    if (selectedRows.length > 1) {
        return;
    }

    const selectedRow = selectedRows.pop();

    $('#EditModalForm').find('[name="id"]:first').val(selectedRow.id);
    $('#EditModalForm').find('[name="SerialNumber"]:first').val(selectedRow.serialNumber);
    $('#EditModalForm').find('[name="Brand"]:first').val(selectedRow.brand);
    $('#EditModalForm').find('[name="WarrantyExpirationDate"]:first').val(new Date(selectedRow.warrantyExpirationDate).toISOString().slice(0, 10));
    $('#EditModalForm').find('[name="Status"]:first').val(selectedRow.status);
}

const DeleteSelectedRows = () => {
    const selectedRows = gridApi.getSelectedRows();

    if (!selectedRows) {
        return;
    }

    try {
        selectedRows.map(row => DeleteAsset(row.id));
        gridApi.applyTransaction({ remove: selectedRows });
    } catch (ex) {
        console.log(ex)
    }

}

const DetailSelectedRow = () => {
    const selectedRows = gridApi.getSelectedRows();

    if (!selectedRows) {
        return;
    }

    // Incase the user didn't selected any row
    if (selectedRows == 0) {
        return;
    }

    // Incase the user selected more then one row
    if (selectedRows.length > 1) {
        return;
    }

    const selectedRow = selectedRows.pop();

    $('#DetailModalBody').empty();
    $('#DetailModalBody').append(
        $('<p>').addClass('fw-semibold').text("Serial Number"),
        $('<p>').text(selectedRow.serialNumber),


        $('<p>').addClass('fw-semibold').text("Brand"),
        $('<p>').text(selectedRow.Brand),


        $('<p>').addClass('fw-semibold').text("Status"),
        $('<p>').text(selectedRow.status),


        $('<p>').addClass('fw-semibold').text("Warranty Expiration Date"),
        $('<p>').text(new Date(selectedRow.warrantyExpirationDate).toLocaleDateString()),
    );

    $('#DetailModal').modal('show');

}

const AddAssetRow = async (SerialNumber, Brand, WarrantyExpirationDate, Status) => {

    try {

        const req = await AddAsset({
            SerialNumber: SerialNumber,
            Brand: Brand,
            WarrantyExpirationDate: WarrantyExpirationDate,
            Status: Status
        });

        if (req.ok) {
            gridApi.applyTransaction({ add: [req] });

            $('#AddModal').modal('hide');
            return;
        }

        // Incase error to show the user
        req.responce.SerialNumber.map(item => $('span[for="SerialNumber"]').text(item))


    } catch (ex) {
        console.log(ex);
    }
}