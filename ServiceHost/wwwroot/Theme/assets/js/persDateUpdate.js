$(document).ready(function () {
    let articlesHtml = document.querySelectorAll('.persDateUpdate');
    for (let index = 0; index < articlesHtml.length; index++) {
        const $date = $(this).find(`#ArticleTime${index}`).val()
        const dateParts = $date.split('/');
        const month = dateParts[1];
        const day = dateParts[2];

        let persianMonth = '';

        if (month === '01')
            persianMonth = 'فروردین'
        if (month === '02')
            persianMonth = 'اردیبهشت'
        if (month === '03')
            persianMonth = 'خرداد'
        if (month === '04')
            persianMonth = 'تیر'
        if (month === '05')
            persianMonth = 'مرداد'
        if (month === '06')
            persianMonth = 'شهریور'
        if (month === '07')
            persianMonth = 'مهر'
        if (month === '08')
            persianMonth = 'آبان'
        if (month === '09')
            persianMonth = 'آذر'
        if (month === '10')
            persianMonth = 'دی'
        if (month === '11')
            persianMonth = 'بهمن'
        if (month === '12')
            persianMonth = 'اسفند'

        $(`.month${index}`).text(persianMonth)
        $(`.day${index}`).text(day)
    }
})
