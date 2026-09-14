"use client";

import { Suspense } from 'react';
import { useRouter, useSearchParams } from 'next/navigation';

const simulationTypes = [
  { value: "find", label: "Find" },
  { value: "race", label: "Race" },
] as const;

const SimulationCards = () => {
  const router = useRouter();
  const searchParams = useSearchParams();
  const selected = searchParams.get('simulation');

  return (
    <div className="grid grid-cols-1 gap-4 sm:grid-cols-2">
      {simulationTypes.map(({ value, label }) => (
        <button
          key={value}
          type="button"
          onClick={() => router.push(`/?simulation=${value}`, { scroll: false })}
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
  );
};

const Page = () => {
  return (
    <main className="min-h-screen flex items-center justify-center pt-18">
      <div className="flex flex-col items-center gap-8 p-5">
        <h1 className="text-2xl font-bold text-foreground">Choose a simulation</h1>
        <Suspense fallback={<div className="h-84 w-56 sm:h-40 sm:w-116" />}>
          <SimulationCards />
        </Suspense>
      </div>
    </main>
  );
};

export default Page;
