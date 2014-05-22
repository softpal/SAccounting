function DateRange(selectedVal)
{
    var fromDate; 
    var toDate;
    //debugger;
    var currDate = new Date();
    var month = ((currDate.getMonth().length + 1) === 1) ? (currDate.getMonth() + 1) : '0' + (currDate.getMonth() + 1);
    var dayDate = ((currDate.getDate().length) === 1) ? '0' + currDate.getDate() : currDate.getDate();

    if (selectedVal == 'Last Year') {            
        var year = currDate.getFullYear();
        fromDate = '01/01/'+(year-1);//'01/01/2013'; 
        toDate = '12/31/' + (year - 1);//'012/31/2013';
    }

    else if (selectedVal == 'last Quarter') {
        //var month = ((currDate.getMonth().length + 1) === 1) ? (currDate.getMonth() + 1) : '0' + (currDate.getMonth() + 1);
        var quarter;
        if (month >= '01' && month <= '03') {
            quarter = '1';
            fromDate = '10/01/' + (currDate.getFullYear()-1);
            toDate = '12/31/' + (currDate.getFullYear()-1);
        }
        else if (month >= '04' && month <= "06") {
            quarter = '2';
            fromDate = '01/01/' + currDate.getFullYear();
            toDate = '03/31/' + currDate.getFullYear();
        }
        else if (month >= '07' && month <= "09") {
            quarter = '3';
            fromDate = '04/01/' + currDate.getFullYear();
            toDate = '06/30/' + currDate.getFullYear();
        }
        else if (month >= '10' && month <= "12") {
            quarter = '4';
            fromDate = '07/01/' + currDate.getFullYear();
            toDate = '09/30/' + currDate.getFullYear();
        }
           
    }

    else if (selectedVal == 'Last Month') {

        var lstMonth = (month - 1);
        var lstDate;
        if (lstMonth == '02' || lstMonth == '04' || lstMonth == '06' || lstMonth == '09' || lstMonth == '11') {
            lstDate = '30';
        }
        else if (lstMonth == '01' || lstMonth == '03' || lstMonth == '05' || lstMonth == '07' || lstMonth == '08' || lstMonth == '10' || lstMonth == '12')
        {
            lstDate = '31';
        }

        fromDate = lstMonth + '/01/' + currDate.getFullYear();
        toDate = lstMonth +'/'+ lstDate+'/' + currDate.getFullYear();

    }

    else if (selectedVal == 'Last Week') {

        var day = currDate.getDay();
        var weekStrt;
        switch (day) {
            case 0: weekStrt = dayDate; break;
            case 1: weekStrt = (dayDate - 1); break;
            case 2: weekStrt = (dayDate - 2); break;
            case 3: weekStrt = (dayDate - 3); break;
            case 4: weekStrt = (dayDate - 4); break;
            case 5: weekStrt = (dayDate - 5); break;
            case 6: weekStrt = (dayDate - 6); break;
        }

        fromDate = month + '/' + (weekStrt - 7) + '/' + currDate.getFullYear();

        toDate = month + "/" + (weekStrt -1) + "/" + currDate.getFullYear();
        //fromDate = '05/04/2014'; //sunday date
        //toDate = '05/11/2014';//saturday date
    }

    else if (selectedVal == 'This Year') {

        //var month = ((currDate.getMonth().length + 1) === 1) ? (currDate.getMonth() + 1) : '0' + (currDate.getMonth() + 1);
        //var dayDate = ((currDate.getDate().length) === 1) ? '0' + currDate.getDate() : currDate.getDate();

        fromDate = '01/01/' + currDate.getFullYear();
        toDate = month + "/" + dayDate + "/" + currDate.getFullYear();

    }
    else if (selectedVal == 'This Quarter') {
            
        var quarter;
        if (month >= '01' && month <= "03") {
            quarter = '1';               

            fromDate = '01/01/' + currDate.getFullYear();
            toDate = month + "/" + dayDate + "/" + currDate.getFullYear();
        }

        else if (month >= '04' && month <= "06") {
            quarter = '2';
            fromDate = '04/01/' + currDate.getFullYear();
            toDate = month + "/" + dayDate + "/" + currDate.getFullYear();
        }
        else if (month >= '07' && month <= "09")
        {
            quarter = '3';
            fromDate = '07/01/' + currDate.getFullYear();
            toDate = month + "/" + dayDate + "/" + currDate.getFullYear();
        }
        else if (month >= '10' && month <= "12")
        {
            quarter = '4';
            fromDate = '10/01/' + currDate.getFullYear();
            toDate = month + "/" + dayDate + "/" + currDate.getFullYear();
        }

        //fromDate = '04/01/2014';
        //toDate = '05/17/2014';
    }

    else if (selectedVal == 'Today') {

        fromDate = toDate = month + "/" + dayDate + "/" + currDate.getFullYear();
    }

    else if (selectedVal == 'This Week') {

        var day = currDate.getDay();
        switch(day)
        {
            case 0: fromDate = month + "/" + dayDate + "/" + currDate.getFullYear();break;
            case 1: fromDate = month + "/" + (dayDate - 1) + "/" + currDate.getFullYear(); break;
            case 2: fromDate = month + "/" + (dayDate - 2) + "/" + currDate.getFullYear(); break;
            case 3: fromDate = month + "/" + (dayDate - 3) + "/" + currDate.getFullYear(); break;
            case 4: fromDate = month + "/" + (dayDate - 4) + "/" + currDate.getFullYear(); break;
            case 5: fromDate = month + "/" + (dayDate - 5) + "/" + currDate.getFullYear(); break;
            case 6: fromDate = month + "/" + (dayDate - 6) + "/" + currDate.getFullYear(); break;
        }

        toDate = month + "/" + dayDate + "/" + currDate.getFullYear();
    }
    else if (selectedVal == 'This Month') {

        fromDate = month + "/01/" + currDate.getFullYear();
        toDate = month + "/" + dayDate + "/" + currDate.getFullYear();

    }

    var dates = new Array();
    dates[0] = fromDate;
    dates[1] = toDate;

    return dates;
}