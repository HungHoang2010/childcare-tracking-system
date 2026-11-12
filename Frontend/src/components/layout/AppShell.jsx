import React from "react";
import "./AppShell.css";
import { Link } from "react-router-dom";
import Dashboard from "../../assets/dashboard_icon/home-2.png";
import Teachers from "../../assets/dashboard_icon/teacher.png";
import Logo from "../../assets/Logo/Logo.webp";
function AppShell({ children }) {
  const menuItems = [
    { name: "Dashboard", icon: Dashboard, path: "/dashboard" },
    { name: "Teachers", icon: Teachers, path: "/teachers" },
  ];
  const LogoItem = { name: "Child Care Tracking", icon: Logo, path: "/Logo" };

  return (
    <div className="app-shell">
      <aside className="app-shell__sidebar">
        <header className="app-shell__sidebar-header">
          <div className="app-shell__sidebar-header__logo">
            <Link
              to={LogoItem.path}
              className="app-shell__sidebar-header__logo-link"
            >
              <img
                src={LogoItem.icon}
                alt={LogoItem.name}
                className="app-shell__sidebar-header__logo-image"
              />
            </Link>
          </div>
          <div className="app-shell__sidebar-header__name">
            <Link
              to={LogoItem.path}
              className="app-shell__sidebar-header__name-link"
            >
              {LogoItem.name}
            </Link>
          </div>
        </header>
        <div className="app-shell__sidebar-main">
          <div className="app-shell__sidebar-main__menu">
            <div className="app-shell__sidebar-main__menu__element">
              {menuItems.map((item, index) => (
                <Link
                  key={index}
                  to={item.path}
                  className="app-shell__sidebar-main__menu__element-link"
                >
                  <img
                    src={item.icon}
                    alt={item.name}
                    className="app-shell__sidebar-main__menu__element-icon"
                  />
                  <span className="app-shell__sidebar-main__menu__element-name">
                    {item.name}
                  </span>
                </Link>
              ))}
            </div>
          </div>
        </div>
      </aside>
      <header className="app-shell__header"></header>
      <div className="app-shell__main-content">
        <main>{children}</main>
      </div>
    </div>
  );
}

export default AppShell;
