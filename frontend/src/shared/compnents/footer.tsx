import React, { FunctionComponent, useMemo } from "react";

const Footer: FunctionComponent = () => {
  const year = useMemo<number>(() => new Date().getFullYear(), []);

  return <footer className="p-3">© {year}</footer>;
};

export default Footer;
