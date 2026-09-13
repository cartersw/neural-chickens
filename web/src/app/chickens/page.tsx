"use client";

import React from 'react';
import {useState } from "react";
import { FiSearch } from 'react-icons/fi';

const Page = () => {
    const [chickenId, setChickenId] = useState("");

    return(
        <main className="min-h-screen flex items-center justify-center pt-18">
            <div className="flex flex-col items-center gap-4 w-[min(28rem,90vw)] p-5">
                <label htmlFor="chickenId" className="text-2xl font-bold text-foreground">
                    Enter Chicken ID
                </label>

                <div className="flex items-center gap-3 w-full rounded-xl border-2 border-foreground/15 bg-foreground/5 px-4 py-3 focus-within:border-foreground transition-colors">
                    <FiSearch className="shrink-0 text-foreground/50" aria-hidden />
                    <input
                        id="chickenId"
                        type="text"
                        inputMode="numeric"
                        value={chickenId}
                        onChange={(event) => setChickenId(event.target.value)}
                        placeholder="e.g. 1"
                        className="w-full bg-transparent text-foreground placeholder:text-foreground/40 outline-none"
                    />
                </div>
            </div>
        </main>
    )
}

export default Page
