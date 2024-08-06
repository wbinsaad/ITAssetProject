window.addEventListener('DOMContentLoaded', () => {
    LoadAssetsByBrand();
    LoadAssetsByStatus();
});

const LoadAssetsByBrand = async () => {
    try {
        const req = await GetAssetsByBrand();

        Highcharts.chart('containerAssetsByBrand', {
            chart: {
                type: 'bar'
            },
            title: {
                text: 'Assets By Brand'
            },
            xAxis: {
                categories: req.map(item => item.brandName)
            },
            plotOptions: {
                series: {
                    borderColor: '#303030',
                    shadow: true
                }
            },
            series: [
                {
                    name: 'Assets',
                    colorByPoint: true,
                    data: req.map(x => { return { name: x.brandName, y: x.numberOfAssets } })
                }
            ]
        });

    } catch (ex) {
        alert(ex)
        return;
    }



}

const LoadAssetsByStatus = async () => {
    const req = await GetAssetsByStatus();

    Highcharts.chart('containerAssetsByStatus', {
        chart: {
            type: 'bar'
        },
        title: {
            text: 'Assets By Status'
        },
        xAxis: {
            categories: req.map(item => item.status)
        },
        series: [
            {
                name: 'Assets',
                colorByPoint: true,
                data: req.map(x => {
                    return {
                        name: x.status,
                        y: x.numberOfAssets
                    }
                })
            }
        ]
    });
}