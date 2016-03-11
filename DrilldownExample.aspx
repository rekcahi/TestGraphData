<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrilldownExample.aspx.cs" Inherits="ChartBindTest.DrilldownExample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
   <%-- <script src="scripts/highcharts.js" type="text/javascript"></script>--%>



<script src="https://code.highcharts.com/highcharts.js"></script>
<script src="https://code.highcharts.com/modules/drilldown.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {
            //"{'metermap':"+ JSON.stringify(modemID) + "}"
            $('#btnYear').click(function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "WebServiceChart.asmx/GetByYear",
                    data: "{}",
                    dataType: "json",
                    success: function (Result) {
             
                        Result = Result.d;
                        var data = [];

                        for (var i in Result) {
                     
                            for (var j = 0; j < Result[i].MeterDate.length; j++) {
                                
                                $('#lbldate').html(Result[i].year);
                                if (j > 0)
                                {
                                    var serie = { name: Result[i].MeterDate[j], y: Result[i].AktifEnergyT[j], drilldown: Result[i].drilldown };
                                    data.push(serie);

                                }
                             
                            }
                         
                        }

                        BindChart(data);
                    },
                    error: function (Result) {
                        alert("Error");
                    }
                });
                return false;
            });

            function GetDateData(yearData)
            {
                var obj = {};
                obj.yearData = yearData;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "WebServiceChart.asmx/GetByYearParameter",

                    data: JSON.stringify(obj),
                    //data: "{'yearData':" + JSON.stringify(yearData) + "}",
                    //"{'metermap':"+ JSON.stringify(modemID) + "}"

                    dataType: "json",
                    success: function (Result) {

                        Result = Result.d;
                        var data = [];

                        for (var i in Result) {

                            for (var j = 0; j < Result[i].MeterDate.length; j++) {

                                $('#lbldate').html(Result[i].year);
                                if (j > 0) {
                                    var serie = { name: Result[i].MeterDate[j], y: Result[i].AktifEnergyT[j], drilldown: Result[i].drilldown };
                                    data.push(serie);

                                }

                            }

                        }

                        BindChart(data);
                    },
                    error: function (Result) {
                        alert("Error");
                    }
                });


            }
            function GetMonthDataParameter(yearData, monthData) {
                var obj = {};
                obj.yearData = yearData;
                obj.monthData = monthData;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "WebServiceChart.asmx/GetMonthDataParameter",
                    data: JSON.stringify(obj),

                    //data: "{'yearData':" + JSON.stringify(yearData) + "}",
                    //"{'metermap':"+ JSON.stringify(modemID) + "}"

                    dataType: "json",
                    success: function (Result) {

                        Result = Result.d;
                        var data = [];

                        for (var i in Result) {

                            for (var j = 0; j < Result[i].MeterDate.length; j++) {

                             //   $('#lbldate').html(Result[i].month_name + " " + Result[i].year);
                                if (j > 0) {
                                    var serie = { name: Result[i].MeterDate[j], y: Result[i].AktifEnergyT[j], drilldown: Result[i].drilldown };
                                    data.push(serie);

                                }

                            }

                        }

                        BindChart(data);
                    },
                    error: function (Result) {
                        alert("Error");
                    }
                });
             }

            $('#btnnext').click(function () {

                var strDate = $('#lbldate').text();
                var strCount = strDate.length;

                var strDateFirstChar =parseInt(strDate.charAt(0));//ilk karakteri al
             
                var truOrFalse = Number.isInteger(strDateFirstChar);
             
                if (strCount == 4)//onyly year
                {
                     var yearData = parseInt(strDate);
                    yearData += 1;
                    GetDateData(yearData)
                    $('#lbldate').html(yearData);
                    return false;
                }
                else if (!truOrFalse)
                {
                    //Mart 2016--->Nisan olacak
                    var array = new Array();
                    array = strDate.split(' ');
                    //alert(array[0] + " " + array[1]);
                    switch (array[0]) {
                        case "Ocak":
                            array[0] = "Şubat";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1],2 );
                            break;
                        case "Şubat":
                            array[0] = "Mart";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1],3);
                            break;
                        case "Mart":
                            array[0] = "Nisan";
                            $('#lbldate').html(array[0] + " " + array[1]);
                           GetMonthDataParameter(array[1],4);
                            break;
                        case "Nisan":
                            array[0] = "Mayıs";
                            $('#lbldate').html(array[0] + " " + array[1]);
                           GetMonthDataParameter(array[1],5);
                            break;
                        case "Mayıs":
                            array[0] = "Haziran";
                            $('#lbldate').html(array[0] + " " + array[1]);
                           GetMonthDataParameter(array[1],6);
                            break;
                        case "Haziran":
                            array[0] = "Temmuz";
                            $('#lbldate').html(array[0] + " " + array[1]);
                           GetMonthDataParameter(array[1],7);
                            break;
                        case "Temmuz":
                            array[0] = "Ağustos";
                            $('#lbldate').html(array[0] + " " + array[1]);
                           GetMonthDataParameter(array[1],8);
                            break;
                        case "Ağustos":
                            array[0] = "Eylül";
                            $('#lbldate').html(array[0] + " " + array[1]);
                           GetMonthDataParameter(array[1],9);
                            break;
                        case "Eylül":
                            array[0] = "Ekim";
                            $('#lbldate').html(array[0] + " " + array[1]);
                           GetMonthDataParameter(array[1],10);
                            break;
                        case "Ekim":
                            array[0] = "Kasım";
                            $('#lbldate').html(array[0] + " " + array[1]);
                           GetMonthDataParameter(array[1],11);
                            break;
                        case "Kasım":
                            array[0] = "Aralık";
                            $('#lbldate').html(array[0] + " " + array[1]);
                           GetMonthDataParameter(array[1],12);
                            break;
                        case "Aralık":
                           GetMonthDataParameter(array[1],12);
                            break;
                     
                        default:

                    }
                   
                }

                return false;
            });

            $('#btnprev').click(function () {

                //var yearData = parseInt($('#lbldate').text());
                //yearData -= 1;
                //GetDateData(yearData)
                //$('#lbldate').html(yearData);
                var strDate = $('#lbldate').text();
                var strCount = strDate.length;

                var strDateFirstChar = parseInt(strDate.charAt(0));//ilk karakteri al

                var truOrFalse = Number.isInteger(strDateFirstChar);

                if (strCount == 4)//onyly year
                {
                    var yearData = parseInt(strDate);
                    yearData -= 1;
                    GetDateData(yearData)
                    $('#lbldate').html(yearData);
                    return false;
                }
                else if (!truOrFalse) {
                    //Mart 2016--->Nisan olacak
                    var array = new Array();
                    array = strDate.split(' ');
                    //alert(array[0] + " " + array[1]);
                    switch (array[0]) {
                        case "Ocak":
                            array[0] = "Şubat";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1], 2);
                            break;
                        case "Şubat":
                            array[0] = "Ocak";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1], 1);
                            break;
                        case "Mart":
                            array[0] = "Şubat";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1], 2);
                            break;
                        case "Nisan":
                            array[0] = "Mart";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1], 3);
                            break;
                        case "Mayıs":
                            array[0] = "Nisan";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1], 4);
                            break;
                        case "Haziran":
                            array[0] = "Mayıs";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1], 5);
                            break;
                        case "Temmuz":
                            array[0] = "Haziran";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1], 6);
                            break;
                        case "Ağustos":
                            array[0] = "Temmuz";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1], 7);
                            break;
                        case "Eylül":
                            array[0] = "Ağustos";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1], 8);
                            break;
                        case "Ekim":
                            array[0] = "Eylül";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1], 9);
                            break;
                        case "Kasım":
                            array[0] = "Ekim";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1], 10);
                            break;
                        case "Aralık":
                            array[0] = "Kasım";
                            $('#lbldate').html(array[0] + " " + array[1]);
                            GetMonthDataParameter(array[1], 11);
                            break;

                        default:

                    }

                }

                return false;
            });
            $('#btnmonth').click(function () {
                var strDate = $('#lbldate').text();


                var d = new Date();
                var month = new Array();
                month[0] = "";
                month[1] = "Ocak";
                month[2] = "Şubat";
                month[3] = "Mart";
                month[4] = "Nisan";
                month[5] = "MAyıs";
                month[6] = "Haziran";
                month[7] = "Temmuz";
                month[8] = "Ağustos";
                month[9] = "Eylül";
                month[10] = "Ekim";
                month[11] = "Kasım";
                month[12] = "Aralık";

                if (strDate.length == 4)
                {
                    var yearDate = parseInt(strDate);
                    var dayCount = d.getMonth() + 1
                    var monthname = month[dayCount];

                    GetMonthDataParameter(yearDate, dayCount);
                    $('#lbldate').html(monthname + " " + yearDate);
                }
             
               

                return false;
                //$.ajax({

                //    type: "POST",
                //    contentType: "application/json; charset=utf-8",
                //    url: "WebServiceChart.asmx/GetMonthData",
                //    data: "{}",
                //    dataType: "json",
                //    success: function (Result) {

                //        Result = Result.d;
                       
                //        var data = [];

                //        for (var i in Result) {
                           
                //            $('#lbldate').html(Result[i].month_name + " " + Result[i].year);
                         
                //            for (var j = 0; j < Result[i].MeterDate.length; j++) {

                //                if (j > 0)
                //                {
                //                    var serie = { name: Result[i].MeterDate[j], y: Result[i].AktifEnergyT[j], drilldown: Result[i].drilldown };
                //                    data.push(serie);

                //                }
                              
                //            }
                          
                //        }

                //        BindChart(data);
                //    },
                //    error: function (Result) {
                //        alert("Error");
                //    }
                //});
                //return false;
            });
            $('#btnweek').click(function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "WebServiceChart.asmx/GetWeekData",
                    data: "{}",
                    dataType: "json",
                    success: function (Result) {

                        Result = Result.d;
                        var data = [];

                        for (var i in Result) {

                            for (var j = 0; j < Result[i].MeterDate.length; j++) {

                                $('#lbldate').html(Result[i].startWeekDays + " " + Result[i].month_name+"-"+ Result[i].andWeekDays + " " + Result[i].month_name + " " + Result[i].year);

                                if (j > 0) {
                                    var serie = { name: Result[i].MeterDate[j], y: Result[i].AktifEnergyT[j], drilldown: Result[i].drilldown };
                                    data.push(serie);

                                }

                            }
                        
                        }

                        BindChart(data);
                    },
                    error: function (Result) {
                        alert("Error");
                    }
                });
                return false;
            });

        });

        function BindChart(seriesArr) {
            $('#container').highcharts({
                chart: {
                    type: 'column',
                   // backgroundColor: '#FCFFC5',
                    //polar: true,
                  
                    backgroundColor: '#579DCB',
                    borderColor: '#6495ED',
                    borderWidth: 2,
                    className: 'dark-container',
                    plotBackgroundColor: '#F0FFF0',
                    plotBorderColor: '#0671B8',
                    plotBorderWidth: 1,

                    events: {

                        drilldown: function (e) {
                            if (!e.seriesOptions) {
                                var chart = this;
                                chart.showLoading('Loading Quarter wise Revenue ...');
                                var dataArr = CallChild(e.point.name);
                                chart.setTitle({
                                    text: 'MDM'
                                });
                                data = {
                                    name: e.point.name,
                                    data: dataArr
                                }
                                setTimeout(function () {
                                    chart.hideLoading();
                                    chart.addSeriesAsDrilldown(e.point, data);
                                }, 1000);
                            }
                        }
                    }
                },
                title: {
                    text: 'MDM Graph'
                },
                xAxis: {
                    type: 'category'
                },


                plotOptions: {
                    series: {
                        borderRadius: 5,
                  
                        dataLabels: {
                            enabled: true
                        }
                    }
                },
                series: [{
                    name: 'Year',
                    colorByPoint: false,
                    pointWidth: 10,
                   
                    data: seriesArr
                }],

                drilldown: {
                    series: [{
                      pointWidth: 100
                    }]
                }
            });
        }

        function CallChild(name) {
            var Drilldowndata = [];
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "WebServiceChart.asmx/GetRevenueByQuarter",
                data: JSON.stringify({ year: name }),
                dataType: "json",
                success: function (Result) {
                    Result = Result.d;
                    for (var i in Result) {
                        var serie = { name: Result[i].year, y: Result[i].amount };
                        Drilldowndata.push(serie);
                    }
                },
                error: function (Result) {
                    alert("Error");
                }
            })
            return Drilldowndata;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <button id="btnYear" class="autocompare">Year</button>
    <button id="btnmonth" class="autocompare">Month</button>
    <button id="btnweek" class="autocompare">Week</button>

        <button id="btnprev" class="autocompare"><<</button>
        <label for="myalue" id="lbldate" runat="server" style="vertical-align: middle"></label>
        <button id="btnnext" class="autocompare">>></button>
  <%--  <asp:Label  ID="lbldate" runat="server" />--%>

    <div id="container" style="width 1000px; height: 400px;">
    </div>
    </form>
</body>
</html>

