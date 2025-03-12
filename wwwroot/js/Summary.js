document.addEventListener("DOMContentLoaded", function () {

    var ctx = document.getElementById('financeChart').getContext('2d');
    // Dynamic data colors
    var isYearly = labels.length === 3; 

    var expenseColors = labels.map((label, index) =>
        isYearly
            ? (label == currentLabel ? 'rgba(255, 99, 132, 0.8)' : 'rgba(255, 99, 132, 0.5)')
            : (index + 1 === currentLabel ? 'rgba(255, 99, 132, 0.8)' : 'rgba(255, 99, 132, 0.5)')
    );

    var incomeColors = labels.map((label, index) =>
        isYearly
            ? (label == currentLabel ? 'rgba(50, 205, 50, 0.8)' : 'rgba(50, 205, 50, 0.5)')
            : (index + 1 === currentLabel ? 'rgba(50, 205, 50, 0.8)' : 'rgba(50, 205, 50, 0.5)')
    );

    var financeChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [
                {
                    label: 'Wydatki',
                    data: expenses,
                    backgroundColor: expenseColors,
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 2
                },
                {
                    label: 'Przychody',
                    data: income,
                    backgroundColor: incomeColors,
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 2
                }
            ]
        },
        options: {
            responsive: true,
            plugins: {
                legend: { position: 'bottom' },
                title: {
                    display: true,
                    text: 'Wydatki i Przychody'
                }
            },
            scales: {
                y: { beginAtZero: true }
            }
        }
    });

    // Category table 
    document.querySelectorAll(".clickable-row").forEach(row => {
        row.addEventListener("click", function () {
            let target = document.querySelector(this.dataset.target);
            target.classList.toggle("show");
        });
    });
});
