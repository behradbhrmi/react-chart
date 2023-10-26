import React, { useEffect, useRef } from 'react';
import Highcharts, { stockChart } from 'highcharts/highstock';
import moment from 'moment';
import flatpickr from 'flatpickr';
import 'flatpickr/dist/flatpickr.min.css';

const ProcessData = ({ symbol, timeFrame, startDate, endDate, pivots, candles }) => {
  const containerRef = useRef(null);
  const startdatepickerRef = useRef(null);
  const enddatepickerRef = useRef(null);

  useEffect(() => {
    const chartOptions = {
      rangeSelector: {
        selected: 1
      },
      title: {
        text: 'Major Pivots Chart'
      },
      plotOptions: {
        candlestick: {
          color: '#CF304A',
          upColor: '#0ECB81'
        }
      },
      series: [
        {
          type: 'line',
          name: 'Pivots',
          data: getPivotPointsData(),
          marker: {
            enabled: true,
            radius: 3
          },
          shadow: true,
          tooltip: {
            valueDecimals: 2
          }
        },
        {
          type: 'candlestick',
          name: 'Candles',
          data: candles,
        }
      ]
    };

    Highcharts.stockChart(containerRef.current, chartOptions);
  }, [pivots, candles]);

  useEffect(() => {
    startdatepickerRef.current = flatpickr("#startdatepicker", {
      enableTime: true,
      dateFormat: "Y-m-d H:i",
      defaultHour: 0,
      defaultMinute: 0,
    });

    enddatepickerRef.current = flatpickr("#enddatepicker", {
      enableTime: true,
      dateFormat: "Y-m-d H:i",
      defaultHour: 0,
      defaultMinute: 0,
    });
  }, []);

  const getPivotPointsData = () => {
    const pivotPointsData = [];

    for (var i = 0; i < candles.length; i++) {
      candles[i]['x'] = unixTS(candles[i].candleDate);
    }

    for (let i = 0; i < pivots.length; i++) {
      var pivot = pivots[i];
      pivot['x'] = unixTS(pivot.candleDate);

      for (let j = 0; j < candles.length; j++) {
        const candle = candles[j];

        if (candle.x === pivot.x) {
          console.log("pivot detected")
          if (pivot.isCeiling) {
            pivotPointsData.push([pivot.x, candle.high]);
          } else {
            pivotPointsData.push([pivot.x, candle.low]);
          }
        }
      }
    }

    return pivotPointsData;
  };

  const unixTS = (datetimeString) => {
    return moment.utc(datetimeString).valueOf();
  };

  return (
    <div className="row mt-3">
      <div ref={containerRef} id="container"></div>
    </div>
  );
};

export default ProcessData;
