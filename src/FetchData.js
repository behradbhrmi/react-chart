import React, { useEffect, useState } from 'react';

const FetchData = ({ onFetch }) => {
  const [symbols, setSymbols] = useState([]);

  useEffect(() => {
    const fetchSymbols = async () => {
      try {
        const baseUrl = 'https://localhost:7218';
        const SymbolsUrl = `${baseUrl}/Candle/GetAllSymbols`;

        const response = await fetch(SymbolsUrl);
        const data = await response.json();
        setSymbols(data);
      } catch (error) {
        console.error("Couldn't read symbols", error);
      }
    };

    fetchSymbols();
  }, []);

  const handleFetchClick = () => {
    const symbolName = document.getElementById("symbolName").value;
    const timeFrame = document.getElementById("timeFrame").value;
    const startDate = document.getElementById("startdatepicker").value;
    const endDate = document.getElementById("enddatepicker").value;

    onFetch(symbolName, timeFrame, startDate, endDate);
  };

  return (
    <div className="row">
      <div className="col">
        <label className="form-label" htmlFor="timeFrame">
          Time Frame:
        </label>
        <select id="timeFrame" className="form-select" title="Time Frame">
          <option value="1m">1m</option>
          <option value="3m">3m</option>
          <option value="5m">5m</option>
          <option value="15m">15m</option>
          <option value="30m">30m</option>
          <option value="1H">1H</option>
          <option value="2H">2H</option>
          <option value="4H">4H</option>
        </select>
      </div>
      <div className="col">
        <label className="form-label" htmlFor="symbolName">
          Symbol:
        </label>
        <select id="symbolName" className="form-select" title="Symbol">
          {symbols.map((symbol) => (
            <option key={symbol.symbolName} value={symbol.symbolName}>
              {symbol.symbolName}
            </option>
          ))}
        </select>
      </div>
      <div className="col">
        <label className="form-label" htmlFor="startdatepicker">
          Start Date:
        </label>
        <input
          type="text"
          id="startdatepicker"
          className="form-control"
          placeholder="Select date"
        />
      </div>
      <div className="col">
        <label className="form-label" htmlFor="enddatepicker">
          End Date:
        </label>
        <input
          type="text"
          id="enddatepicker"
          className="form-control"
          placeholder="Select date"
        />
      </div>
      <div className="col d-flex">
        <button
          id="renderButton"
          type="button"
          className="btn btn-success align-self-end"
          onClick={handleFetchClick}
        >
          Fetch Data
        </button>
      </div>
    </div>
  );
};

export default FetchData;
