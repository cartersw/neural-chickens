'use client'

import Link from 'next/link'
import { useEffect, useRef, useState } from 'react'
import { FiChevronDown } from 'react-icons/fi'

type Tab = {
    href: string
    label: string
}

const tabs: Tab[] = [
    { href: '/chickens', label: 'Chickens' },
    { href: '/about', label: 'About' },
]

const simulations: Tab[] = [
    { href: '/?simulation=find', label: 'Find' },
    { href: '/?simulation=race', label: 'Race' },
]

const Navbar = () => {
    const [isOpen, setIsOpen] = useState(false)
    const dropdownRef = useRef<HTMLLIElement>(null)
    const triggerRef = useRef<HTMLButtonElement>(null)

    useEffect(() => {
        if (!isOpen) return

        const handlePointerDown = (event: PointerEvent) => {
            if (event.target instanceof Node && !dropdownRef.current?.contains(event.target)) {
                setIsOpen(false)
            }
        }

        document.addEventListener('pointerdown', handlePointerDown)
        return () => document.removeEventListener('pointerdown', handlePointerDown)
    }, [isOpen])

    return (
        <nav className="fixed top-0 inset-x-0 z-50 bg-background border-b border-foreground/10">
            <div className="flex items-center gap-8 px-6 py-3">
                <Link
                    href="/"
                    className="text-xl font-bold text-foreground"
                >
                    Neural Chickens
                </Link>

                <ul className="flex items-center gap-1">
                    <li
                        ref={dropdownRef}
                        className="relative"
                        onBlur={(event) => {
                            if (!event.currentTarget.contains(event.relatedTarget)) {
                                setIsOpen(false)
                            }
                        }}
                        onKeyDown={(event) => {
                            if (event.key === 'Escape' && isOpen) {
                                event.preventDefault()
                                setIsOpen(false)
                                triggerRef.current?.focus()
                            }
                        }}
                    >
                        <button
                            ref={triggerRef}
                            type="button"
                            aria-expanded={isOpen}
                            aria-controls="simulations-dropdown"
                            onClick={() => setIsOpen((open) => !open)}
                            className="flex cursor-pointer items-center gap-2 rounded-full px-4 py-2 text-sm font-bold text-foreground transition-colors hover:bg-brand-soft aria-expanded:bg-brand-soft focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-foreground"
                        >
                            Simulations
                            <FiChevronDown
                                aria-hidden="true"
                                className={`transition-transform ${isOpen ? 'rotate-180' : ''}`}
                            />
                        </button>
                        <ul
                            id="simulations-dropdown"
                            hidden={!isOpen}
                            className="absolute left-0 top-full mt-2 min-w-40 rounded-xl border border-foreground/10 bg-background p-1 shadow-lg"
                        >
                            {simulations.map(({ href, label }) => (
                                <li key={href}>
                                    <Link
                                        href={href}
                                        onClick={() => setIsOpen(false)}
                                        className="block rounded-lg px-4 py-2 text-sm font-bold text-foreground transition-colors hover:bg-brand-soft focus-visible:bg-brand-soft focus-visible:outline-2 focus-visible:outline-foreground"
                                    >
                                        {label}
                                    </Link>
                                </li>
                            ))}
                        </ul>
                    </li>
                    {tabs.map(({ href, label}) => (
                        <li key={href}>
                            <Link
                                href={href}
                                className="flex items-center gap-2 rounded-full px-4 py-2 text-sm font-bold text-foreground transition-colors hover:bg-brand-soft"
                            >
                                {label}
                            </Link>
                        </li>
                    ))}
                </ul>
            </div>
        </nav>
    )
}

export default Navbar
