import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import Header from "./shared/compnents/header";
import Footer from "./shared/compnents/footer";
import { Router, View } from "react-navi";
import { mount, route } from "navi";
import HomePage from "./pages/home/home";

const routes = mount({
  "/": route({
    title: "Home",
    view: <HomePage />
  })
});

const App: React.FC = () => {
  return (
    <Router routes={routes}>
      <Header />
      <View />
      <Footer />
    </Router>
  );
};

export default App;
