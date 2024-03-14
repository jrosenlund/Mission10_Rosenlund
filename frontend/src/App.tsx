import React, { useEffect, useState } from 'react';
import './App.css';
import { Bowler } from './types/Bowler';

function Header() {
  return (
    <>
      <h2 className="text-center mt-4 mb-2">Welcome to the bowling roster!</h2>
      <h4 className="text-center mb-4">
        This page is made specifically for the Marlins and Sharks.
      </h4>
    </>
  );
}

function BowlerList() {
  const [bowlerData, setBowlerData] = useState<Bowler[]>([]);

  useEffect(() => {
    const fetchBowlerData = async () => {
      const rsp = await fetch('http://localhost:5135/api/bowlingleague');
      let b = await rsp.json();
      b = b.filter(
        (bowler: Bowler) =>
          bowler.teamName === 'Marlins' || bowler.teamName === 'Sharks',
      );
      setBowlerData(b);
    };
    fetchBowlerData();
  }, []);

  return (
    <>
      <div>
        <table className="table table-bordered">
          <thead>
            <tr>
              <th>Bowler Name</th>
              <th>Team</th>
              <th>Address</th>
              <th>City</th>
              <th>State</th>
              <th>Zip</th>
              <th>Phone</th>
            </tr>
          </thead>
          <tbody>
            {bowlerData.map((b) => (
              <tr key={b.bowlerId}>
                <td>
                  {b.bowlerLastName}, {b.bowlerFirstName} {b.bowlerMiddleInit}
                </td>
                <td>{b.teamName}</td>
                <td>{b.bowlerAddress}</td>
                <td>{b.bowlerCity}</td>
                <td>{b.bowlerState}</td>
                <td>{b.bowlerZip}</td>
                <td>{b.bowlerPhoneNumber}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </>
  );
}

function App() {
  return (
    <>
      <Header />
      <BowlerList />
    </>
  );
}

export default App;
