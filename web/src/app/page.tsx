"use client";

import React from 'react';
import { useState } from "react";

const simulationTypes = [
  { value: "find", label: "Find" },
  { value: "race", label: "Race" },
] as const;

type SimulationType = (typeof simulationTypes)[number]["value"];

const Page = () => {
  const [selected, setSelected] = useState<SimulationType | null>(null);

  return (
    <main className="min-h-screen flex items-center justify-center pt-18">
      <div className="flex flex-col items-center gap-8 p-5">
        <h1 className="text-2xl font-bold text-foreground">Choose a simulation</h1>

        <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
          {simulationTypes.map(({ value, label }) => (
            <button
              key={value}
              type="button"
              onClick={() => setSelected(value)}
              aria-pressed={selected === value}
              className={`flex h-40 w-56 items-center justify-center rounded-xl border-2 text-lg font-bold text-foreground cursor-pointer transition-colors ${
                selected === value
                  ? "border-foreground bg-brand-soft"
                  : "border-foreground/15 bg-foreground/5 hover:bg-brand-soft"
              }`}
            >
              {label}
            </button>
          ))}
        </div>
      </div>
    </main>
  );
};

export default Page;
