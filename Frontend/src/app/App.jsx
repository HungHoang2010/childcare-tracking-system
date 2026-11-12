import { useState } from "react";
import AppShell from "../components/layout/AppShell";
import SchoolDashboard from "../features/school/pages/SchoolDashboard";

function App() {
  const [count, setCount] = useState(0);

  return (
    <AppShell>
      <SchoolDashboard />
    </AppShell>
  );
}

export default App;
