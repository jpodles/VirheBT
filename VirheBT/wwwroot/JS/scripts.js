function GeneratePDF() {
    var element = document.getElementById('dashboard');
    var opt = {
        filename: 'report.pdf',
        margin: 3,
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 1 },
        jsPDF: { unit: 'mm', format: 'a3', orientation: 'landscape' }
    };

    html2pdf().set(opt).from(element).save();
}