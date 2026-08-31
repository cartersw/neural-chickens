import Link from 'next/link'
import React from 'react'
import { IconType } from 'react-icons'
import { FiInfo, FiPlay } from 'react-icons/fi'
import { GiChicken } from 'react-icons/gi'

type Tab = {
    href: string
    label: string
}

const tabs: Tab[] = [
    { href: '/', label: 'Simulate'},
    { href: '/chickens', label: 'Chickens' },
    { href: '/about', label: 'About' },
]

const Navbar = () => {
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
