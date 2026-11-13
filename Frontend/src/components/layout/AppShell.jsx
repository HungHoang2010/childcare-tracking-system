import React from "react";
import "./AppShell.css";
import { Link } from "react-router-dom";
import Dashboard from "../../assets/dashboard_icon/home-2.png";
import Teachers from "../../assets/dashboard_icon/teacher.png";
import Logo from "../../assets/Logo/Logo.webp";
import Notification from "../../assets/dashboard_icon/notification.png";
import Chats from "../../assets/dashboard_icon/chats.png";
import Settings from "../../assets/dashboard_icon/settings.png";
import { useState, useEffect, useCallback, useRef } from "react";
function AppShell({ children }) {
  const menuItems = [
    { name: "Dashboard", icon: Dashboard, path: "/dashboard" },
    { name: "Teachers", icon: Teachers, path: "/teachers" },
  ];
  const LogoItem = { name: "Child Care Tracking", icon: Logo, path: "/Logo" };
  const panelLeftClose = {
    name: "Close sidebar",
    icon: <i class="fa-solid fa-arrow-left"></i>,
  };
  const headerSearchIcon = {
    name: "Search",
    icon: <i class="fa-solid fa-magnifying-glass"></i>,
  };
  const headerNotificationIcon = {
    name: "Notifications",
    icon: Notification,
    path: "/notification",
  };
  const headerChatsIcon = {
    name: "Chats",
    icon: Chats,
    path: "/chats",
  };
  const headerSettingsIcon = {
    name: "Settings",
    icon: Settings,
    path: "/settings",
  };
  const dropdownRef = useRef(null);
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const toggleDropdown = () => {
    setDropdownOpen((prev) => !prev);
  };

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
      <header className="app-shell__header">
        <div className="app-shell__header-left">
          <button
            type="button"
            aria-label={panelLeftClose.name}
            className="app-shell__header-left__panelLeftClose"
          >
            {panelLeftClose.icon}
          </button>
          <form role="search" className="app-shell__header-left__searchBar">
            <button
              type="submit"
              aria-label={headerSearchIcon.name}
              className="app-shell__header-left__searchBar-icon"
            >
              {headerSearchIcon.icon}
            </button>
            <input
              type="search"
              placeholder="Tìm kiếm..."
              aria-label="Tìm kiếm"
              className="app-shell__header-left__searchBar-input"
            />
          </form>
        </div>

        <div className="app-shell__header-right">
          <div className="app-shell__header-right__options">
            <Link
              to={headerNotificationIcon.path}
              aria-label={headerNotificationIcon.name}
              className="app-shell__header-right__options__notification-button"
            >
              <img
                src={headerNotificationIcon.icon}
                className="app-shell__header-right__options__notification-icon"
                alt={headerNotificationIcon.name}
              />
            </Link>
            <Link
              to={headerChatsIcon.path}
              aria-label={headerChatsIcon.name}
              className="app-shell__header-right__options__chats-button"
            >
              <img
                src={headerChatsIcon.icon}
                className="app-shell__header-right__options__chats-icon"
                alt={headerChatsIcon.name}
              />
            </Link>
            <Link
              to={headerSettingsIcon.path}
              aria-label={headerSettingsIcon.name}
              className="app-shell__header-right__options__settings-button"
            >
              <img
                src={headerSettingsIcon.icon}
                className="app-shell__header-right__options__settings-icon"
                alt={headerSettingsIcon.name}
              />
            </Link>
            <div className="app-shell__header-right__options__profile">
              <img
                src="https://via.placeholder.com/40"
                alt="User Profile"
                className="app-shell__header-right__options__profile-image"
                onClick={toggleDropdown}
                ref={dropdownRef}
              />
              {dropdownOpen && (
                <div className="app-shell__header-right__options__profile-image__dropdown">
                  <p>Xin chào, User!</p>
                  <Link to="/profile">Hồ sơ</Link>
                  <Link to="/settings">Cài đặt</Link>
                  <Link to="/logout">Đăng xuất</Link>
                </div>
              )}
            </div>
          </div>
        </div>
      </header>
      <div className="app-shell__main-content">
        <main>{children}</main>
      </div>
    </div>
  );
}

export default AppShell;
