import React, { FunctionComponent } from "react";

const HomePage: FunctionComponent = () => (
  <div className="jumbotron">
    <h1 className="display-4">Willkommen!</h1>
    <p className="lead">Schön, dass Sie customer-app aufgerufen haben.</p>
    <hr className="my-4" />
    <p>
      Verwalten Sie Ihre Kunden wie noch nie zuvor. Dank React im Frontend und
      .NET Core im Backend.
    </p>
    <p className="lead">
      <a className="btn btn-primary btn-lg" href="/" role="button">
        Zur Kundenliste
      </a>
    </p>
  </div>
);

export default HomePage;
