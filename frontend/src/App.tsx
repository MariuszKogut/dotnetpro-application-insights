import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import Header from "./shared/compnents/header";
import Footer from "./shared/compnents/footer";
import HomePage from "./pages/home/home";
import { BrowserRouter, Switch, Route } from "react-router-dom";

const App: React.FC = () => {
  return (
    <BrowserRouter>
      <Header />
      <Switch>
        <Route path="/" exact={true}>
          <HomePage />
        </Route>
      </Switch>
      <Footer />
    </BrowserRouter>
  );
};

export default App;
