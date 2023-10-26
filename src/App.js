import React, { useState } from 'react';
import FetchData from './FetchData';
import ProcessData from './ProcessData';
import 'bootstrap/dist/css/bootstrap.min.css';

const App = () => {
  const [symbol, setSymbol] = useState(null);
  const [timeFrame, setTimeFrame] = useState(null);
  const [startDate, setStartDate] = useState(null);
  const [endDate, setEndDate] = useState(null);
  const [pivots, setPivots] = useState([]);
  const [candles, setCandles] = useState([]);

  const handleFetchData = (symbol, timeFrame, startDate, endDate) => {
    setSymbol(symbol);
    setTimeFrame(timeFrame);
    setStartDate(startDate);
    setEndDate(endDate);

    fetchPivots(symbol, timeFrame, startDate, endDate)
      .then((pivots) => {
        setPivots(pivots);
      })
      .catch(() => {
        console.error("Couldn't fetch pivots", error);
      });

    fetchCandles(symbol, timeFrame, startDate, endDate)
      .then((candles) => {
        setCandles(candles);
      })
      .catch(() => {
        console.error("Couldn't fetch candles", error);
      });
  };

  const fetchPivots = async (symbol, timeFrame, startDate, endDate) => {
    try {
      const baseUrl = 'https://localhost:7218';
      const response = await fetch(
        `${baseUrl}/majorpivot/getall?symbol=${symbol}&timeframe=${timeFrame}&startdate=${startDate}&enddate=${endDate}`
      );
      const data = await response.json();
      return data;
    } catch (error) {
      console.error("Couldn't fetch pivots", error);
      return [];
    }
  };

  const fetchCandles = async (symbol, timeFrame, startDate, endDate) => {
    try {
      const baseUrl = 'https://localhost:7218';
      const response = await fetch(
        `${baseUrl}/candle?symbol=${symbol}&timeframe=${timeFrame}&startdate=${startDate}&enddate=${endDate}`
      );
      const data = await response.json();
      return data;
    } catch (error) {
      console.error("Couldn't fetch candles", error);
      return [];
    }
  };

  return (
    <div className="container">
      <h1 className="text-center">Major Pivots</h1>
      <FetchData onFetch={handleFetchData} />
      <ProcessData
        symbol={symbol}
        timeFrame={timeFrame}
        startDate={startDate}
        endDate={endDate}
        pivots={pivots}
        candles={candles}
      />
    </div>
  );
};

export default App;
